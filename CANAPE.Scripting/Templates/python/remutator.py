# This pulls in the canape library namespaces
import CANAPE.Nodes
import CANAPE.DataFrames
import re

# Simple pipeline node to mutate based on a regular expression
class ReMutatorNode(CANAPE.Nodes.BasePipelineNode):

    # Called when a new frame has arrived
    def OnInput(self, frame):
    
        # Get a replacement string from the graph, if set
        replacement = self.Graph.GetProperty("replacement")
        if replacement is None:
            # Get the string from the node
            replacement = self.GetProperty("replacement")
            if replacement is None:
                # Nothing available so default
                replacement = "Goodbye"
        
        # Get per graph value (there is also GlobalMeta which is per service)
        count = self.Graph.Meta.IncrementCounter("count", 1, 1)
            
        # Do a simple regex sub to change Hello to replacement
        s = re.sub("Hello", replacement * count, frame.ToByteString())
                
        # Create a new data frame with the contents of the string
        self.WriteOutput(CANAPE.DataFrames.DataFrame(s))
