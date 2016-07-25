#region License
/* 
 This code is based on Satsuma Graph Library, with modifications to 
 integrate with canape document model
 
 This file is part of Satsuma Graph Library
Copyright © 2013 Balázs Szalkai

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

   1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

   2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

   3. This notice may not be removed or altered from any source
   distribution.*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CANAPE.Documents.Net.NodeConfigs;

namespace CANAPE.Documents.Net
{
	/// An immutable point whose coordinates are \c double.
	internal struct PointD : IEquatable<PointD>
	{
		public double X { get; private set; }
		public double Y { get; private set; }

		public PointD(double x, double y)
			: this()
		{
			X = x;
			Y = y;
		}

		public bool Equals(PointD other)
		{
			return X == other.X && Y == other.Y;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PointD)) return false;
			return Equals((PointD)obj);
		}

		public static bool operator ==(PointD a, PointD b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(PointD a, PointD b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return (X.GetHashCode() * 17) + Y.GetHashCode();
		}

		public string ToString(IFormatProvider provider)
		{
			return string.Format(provider, "({0} {1})", X, Y);
		}

		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		/// Returns the vector sum of two points.
		public static PointD operator +(PointD a, PointD b)
		{
			return new PointD(a.X + b.X, a.Y + b.Y);
		}

		/// Added for CLS compliancy.
		public static PointD Add(PointD a, PointD b)
		{
			return a + b;
		}

		/// Returns the Euclidean distance from another point.
		public double Distance(PointD other)
		{
			return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
		}
	}

	/// Attempts to draw a graph to the plane such that a certain equilibrium is attained.
	/// Models the graph as electrically charged nodes connected with springs.
	/// Nodes are attracted by the springs and repelled by electric forces.
	///
	/// By default, the springs behave logarithmically, and (as in reality) the electric repulsion force is inversely
	/// proportional to the square of the distance.
	/// The formulae for the attraction/repulsion forces can be configured through #SpringForce and #ElectricForce.
	///
	/// The algorithm starts from a given configuration (e.g. a random placement)
	/// and lets the forces move the graph to an equilibrium.
	/// Simulated annealing is used to ensure good convergence.
	/// Each convergence step requires O(n<sup>2</sup>) time, where \e n is the number of the nodes.
	///
	/// Force-directed layout algorithms work best for graphs with a few nodes (under about 100).
	/// Not only because of the running time, but also the probability of running into a poor local minimum 
	/// is quite high for a large graph. This decreases the chance that a nice arrangement is attained.
	///
	/// Example:
	/// \code
	/// var g = new CompleteGraph(7);
	/// var layout = new ForceDirectedLayout(g);
	/// layout.Run();
	/// foreach (var node in g.Nodes())
	///		Console.WriteLine("Node "+node+" is at "+layout.NodePositions[node]);
	/// \endcode
	internal sealed class ForceDirectedLayout
	{
		/// The default initial temperature for the simulated annealing.
		public const double DefaultStartingTemperature = 0.2;
		/// The temperature where the simulated annealing should stop.
		public const double DefaultMinimumTemperature = 0.01;
		/// The ratio between two successive temperatures in the simulated annealing.
		public const double DefaultTemperatureAttenuation = 0.95;

		/// The input graph.
		public NetGraphDocument Graph { get; private set; }
		/// The current layout, which assigns positions to the nodes.
		public Dictionary<BaseNodeConfig, PointD> NodePositions { get; private set; }
		/// The function defining the attraction force between two connected nodes.
		/// Edges are viewed as springs that want to bring the two connected nodes together.
		/// The function takes a single parameter, which is the distance of the two nodes.
		///
		/// The default force function is 2 <em>ln</em>(d).
		public Func<double, double> SpringForce { get; set; }
		/// The function defining the repulsion force between two nodes.
		/// Nodes are viewed as electrically charged particles which repel each other.
		/// The function takes a single parameter, which is the distance of the two nodes.
		///
		/// The default force function is 1/d<sup>2</sup>.
		public Func<double, double> ElectricForce { get; set; }
		/// The current temperature in the simulated annealing.
		public double Temperature { get; set; }
		/// The temperature attenuation factor used during the simulated annealing.
		public double TemperatureAttenuation { get; set; }
		
		public ForceDirectedLayout(NetGraphDocument graph, Func<BaseNodeConfig, PointD> initialPositions = null)
		{
			Graph = graph;
			NodePositions = new Dictionary<BaseNodeConfig, PointD>();
			SpringForce = (d => 2 * Math.Log(d));
			ElectricForce = (d => 1 / (d * d));
			TemperatureAttenuation = DefaultTemperatureAttenuation;
		
			Initialize(initialPositions);
		}

		/// Initializes the layout with the given one and resets the temperature.
		/// \param initialPositions If null, a random layout is used.
		public void Initialize(Func<BaseNodeConfig, PointD> initialPositions = null)
		{
			if (initialPositions == null)
			{
				// make a random layout
				Random r = new Random();
				initialPositions = (node => new PointD(r.NextDouble(), r.NextDouble()));
			}

			foreach (var node in Graph.Nodes)
				NodePositions[node] = initialPositions(node);

			// reset the temperature
			Temperature = DefaultStartingTemperature;
		}

		/// Performs an optimization step.
		public void Step()
		{
			Dictionary<BaseNodeConfig, PointD> forces = new Dictionary<BaseNodeConfig, PointD>();

			foreach (var u in Graph.Nodes)
			{
				PointD uPos = NodePositions[u];
				double xForce = 0, yForce = 0;
				// attraction forces
				foreach (var arc in u.EdgesFrom)
				{
					PointD vPos = NodePositions[arc.DestNode];
					double d = uPos.Distance(vPos);
					double force = Temperature * SpringForce(d);
					xForce += (vPos.X - uPos.X) / d * force;
					yForce += (vPos.Y - uPos.Y) / d * force;
				}
				// repulsion forces
				foreach (var v in Graph.Nodes)
				{
					if (v == u) continue;
					PointD vPos = NodePositions[v];
					double d = uPos.Distance(vPos);
					double force = Temperature * ElectricForce(d);
					xForce += (uPos.X - vPos.X) / d * force;
					yForce += (uPos.Y - vPos.Y) / d * force;
				}
				forces[u] = new PointD(xForce, yForce);
			}

			foreach (var node in Graph.Nodes)
				NodePositions[node] += forces[node];
			Temperature *= TemperatureAttenuation;
		}

		/// Runs the algorithm until a low temperature is reached.
		public void Run(double minimumTemperature = DefaultMinimumTemperature)
		{
			while (Temperature > minimumTemperature) Step();
		}
	}
}
