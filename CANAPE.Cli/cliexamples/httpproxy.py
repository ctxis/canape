import sys
from canapecli import log, packets
from canapecli.docs.net import HttpProxyDocument

doc = HttpProxyDocument()
doc.LocalPort = 3128

logger = log.getverboselogger(sys.stdout)
service = doc.Create(logger)

service.Start()

print "Created", service, "Press Enter to exit..."
sys.stdin.readline()

service.Stop()

print "Captured packets:"

for p in doc.Packets:
    print packets.tostr(p)
