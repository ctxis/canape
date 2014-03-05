# This pulls in the canape library namespaces
import CANAPE.Nodes
import CANAPE.DataFrames

# Simple pipeline node
class PipelineNode(CANAPE.Nodes.BaseDynamicPipelineNode):

    # Called when a new frame has arrived
    def OnInput(self, frame):
        # Create a new data frame with the contents of the string
        self.LogInfo("Received {0}", frame)
        self.WriteOutput(frame)
