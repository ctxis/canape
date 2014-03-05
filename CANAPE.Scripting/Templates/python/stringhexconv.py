# Simple example of a hex + string converter in python

from CANAPE.Utils import BinaryEncoding

# Method to just XOR the byte array
def doconvert(obj):
    for i in range(len(obj)):
        obj[i] = obj[i] ^ 42
    return obj

# Entry point of hex converter, return byte array
def ConvertHex(startPos, data):
    return doconvert(data)

# Entry point for string converter, return string     
def ConvertString(startPos, data):
    return BinaryEncoding.Instance.GetString(doconvert(BinaryEncoding.Instance.GetBytes(args[1])))
