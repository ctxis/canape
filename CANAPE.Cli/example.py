import sys
from CANAPE.Utils import ConsoleUtils
from CANAPE.Documents.Net import FixedProxyDocument

logger = ConsoleUtils.GetVerboseLogger()
doc = FixedProxyDocument()
doc.Host = 'www.google.com'
doc.Port = 80
service = doc.Create(logger)

service.Start()

print "Press Enter to exit..."
sys.stdin.readline()

service.Stop()

print "Captured packets:"

for p in doc.Packets:
    print p.Frame.ToDataString()
