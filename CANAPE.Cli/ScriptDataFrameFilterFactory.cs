using System;
using CANAPE.DataFrames;
using CANAPE.DataFrames.Filters;
using CANAPE.Documents.Net.NodeConfigs;
using CANAPE.NodeFactories;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Cli
{
    public class ScriptDataFrameFilterFactory : IDataFrameFilterFactory
    {
        dynamic _filterFunc;
        int _arrity;
        bool _enabled;

        class ScriptDataFrameFilter : IDataFrameFilter
        {
            dynamic _filterFunc;
            bool _invert;
            int _arrity;

            public ScriptDataFrameFilter(dynamic filterFunc, int arrity)
            {
                _filterFunc = filterFunc;
                _arrity = arrity;
                _invert = true;
            }

            public bool IsMatch(DataFrame frame)
            {
                return IsMatch(frame, null, null, null, Guid.Empty, null);
            }

            public bool IsMatch(DataFrame frame, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, Guid uuid, BasePipelineNode node)
            {
                switch (_arrity)
                {                    
                    case 1:
                        return _filterFunc(frame);
                    case 2:
                        return _filterFunc(frame, meta);
                    case 3:
                        return _filterFunc(frame, meta, globalMeta);
                    case 4:
                        return _filterFunc(frame, meta, globalMeta, properties);
                    case 5:
                        return _filterFunc(frame, meta, globalMeta, properties, uuid);
                    case 6:
                        return _filterFunc(frame, meta, globalMeta, properties, uuid, node);
                    default:
                        break;
                }

                return false;
            }

            public bool Invert
            {
                get
                {
                    return _invert;
                }
                set
                {
                    _invert = value;
                }
            }
        }

        public ScriptDataFrameFilterFactory(dynamic filterFunc, int arrity)
        {
            _filterFunc = filterFunc;
            _arrity = arrity;
            _enabled = true;
        }

        public IDataFrameFilter CreateFilter()
        {
            if (!_enabled)
            {
                Func<DataFrame, bool> f = d => true;

                return new ScriptDataFrameFilter(f, 1);
            }
            else
            {
                return new ScriptDataFrameFilter(_filterFunc, _arrity);
            }
        }


        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public static void RemoveFilter(dynamic filterFunc, BaseNodeConfig config)
        {
            foreach (IDataFrameFilterFactory filter in config.Filters)
            {
                ScriptDataFrameFilterFactory scriptFilter = filter as ScriptDataFrameFilterFactory;
                if (scriptFilter != null)
                {
                    if (filterFunc == scriptFilter._filterFunc)
                    {
                        config.RemoveFilter(filter);
                        break;
                    }
                }
            }
        }
    }
}
