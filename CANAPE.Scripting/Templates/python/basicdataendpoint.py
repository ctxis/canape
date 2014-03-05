from CANAPE.Scripting import BaseDataEndpoint
from CANAPE.DataFrames import DataFrame

class DataEndpoint(BaseDataEndpoint):

    # Run method, should exit when finished
    def Run(self, adapter, logger):  
    
        
      
        frame = adapter.Read()

        while frame is not None:        
            logger.LogInfo("Received {0}", frame)

            # Write it back out again reversed
            adapter.Write(DataFrame(frame.ToByteString()[::-1]))

            frame = adapter.Read();        

    # Gets a textual description of the endpoint
    def get_Description(self):    
        return "Example Reversing Endpoint"
