# This pulls in the canape library namespaces
import CANAPE.Nodes
import CANAPE.Scripting
import CANAPE.DataFrames

# String parser, acts on a finite blob of data passed in one go
class StringParser(CANAPE.Scripting.IDataStringParser):

    # Called when parsing a string
    # data - The string data to parse
    # root - The root DataKey to add parsed values to
    # logger - An object to log information
    def FromString(self, data, root, logger):			
        logger.LogInfo("Added new value {0}", data)
        # Reverse string
        root.AddValue("data", data[::-1])		
        
    # Called when converting parsed data back to a string
    # root - The root DataKey built in FromString
    # logger - An object to log information 
    def ToString(self, root, logger):
        logger.LogInfo("Converting to string")
        node = root.SelectSingleNode("data")
        if node is None:
            logger.LogError("No data value available")
            return ""
        else:
            # Get value and reverse it back to the original direction
            return node.ToByteString()[::-1]
    
    # Called when the data frame is being displayed, can return any data 
    # you want
    def ToDisplayString(self, root, logger):
        node = root.SelectSingleNode("data")
        if node is None:			
            return ""
        else:
            # Get value, don't reverse it
            return node.ToByteString()