# This pulls in the canape library namespaces
import CANAPE.Nodes
import CANAPE.Scripting
import CANAPE.DataFrames

# Stream parser, acts on a continuous stream of data
class StreamParser(CANAPE.Scripting.IDataStreamParser):

    # Called when constructing a new data frame
    # reader - A special stream reader class
    # root - The root DataKey to add parsed values to
    # logger - An object to log information
    def FromReader(self, reader, root, logger):
        # Read up to five bytes (as a string) and reverse it
        s = reader.Read(5, False)[::-1]
        logger.LogInfo("Read {0} from stream", s)
        root.AddValue("data", s)

    # Called when writing the data back to a byte stream
    # writer - A special stream writer class
    # root - The root DataKey to add parsed values to
    # logger - An object to log information
    def ToWriter(self, writer, root, logger):
        logger.LogInfo("Writing to stream")
        node = root.SelectSingleNode("data")
        if node is None:
            logger.LogError("No data value available")
        else:
            # Get value and reverse it back to the original direction
            writer.Write(node.ToByteString()[::-1])

    # Called when the data frame is being displayed, can return any data 
    # you want
    def ToDisplayString(self, root, logger):
        node = root.SelectSingleNode("data")
        if node is None:
            return ""
        else:
            # Get value, don't reverse it
            return node.ToByteString()