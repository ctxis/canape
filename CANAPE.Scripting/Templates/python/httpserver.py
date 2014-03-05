from CANAPE.Scripting import BaseDataEndpoint
from CANAPE.DataFrames import DataFrame
from CANAPE.Net.Protocols.Server import DynamicHttpDataServer, HttpServerResponseData
from CANAPE.Utils import GeneralUtils

# Example HTTP Server
class HttpServer(DynamicHttpDataServer):

    # Handle incoming request
    def HandleRequest(self, method, path, body, headers, version, logger):
        logger.LogInfo("Received Request: {0} {1} {2}", method, path, 
            GeneralUtils.MakeByteString(body))
        return HttpServerResponseData("Hello World")

    # Gets a textual description of the endpoint
    def get_Description(self):    
        return "Example HTTP Server"
