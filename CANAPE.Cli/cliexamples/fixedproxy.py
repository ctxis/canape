import sys
from canapecli import log, packets
from canapecli.docs.net import FixedProxyDocument

doc = FixedProxyDocument()
doc.LocalPort = 12345
doc.Host = 'www.google.com'
doc.Port = 80

logger = log.getverboselogger(sys.stdout)
service = doc.Create(logger)

service.Start()

print "Created", str(service), "Press Enter to exit..."
sys.stdin.readline()

service.Stop()

print "Captured packets:"

for p in doc.Packets:
    print packets.tostr(p)
