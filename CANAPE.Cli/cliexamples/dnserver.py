import sys
from canapecli import log, packets
from canapecli.docs.net import DnsServerDocument

doc = DnsServerDocument()

logger = log.getverboselogger(sys.stdout)
doc.ResponseAddress = "1.1.1.1"
doc.ResponseAddress6 = "1010::1"
doc.ReverseDns = "www.dummy.com"

service = doc.Create(logger)

service.Start()

print "Created", service, "Press Enter to exit..."
sys.stdin.readline()

service.Stop()

