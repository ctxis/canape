# Custom packet exporter, select packets in a log then choose Run Script
# from the right click menu

def ProcessPackets(packets):
    try:
        f = open("packets.csv", "w")
    
        f.write("Timestamp,Tag,Network,Data\n")
        # Iterate over selected packets
        for p in packets:
            f.write("{0},{1},{2},{3}\n".format(p.Timestamp,p.Tag,p.Network,p.Frame.Root.ToDataString()))
        f.close()
    except:
        print "Error"