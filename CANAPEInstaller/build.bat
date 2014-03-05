del *.wixobj *.msi
candle -ext WiXNetFxExtension canape.wxs canape_files.wxs
light -ext WiXNetFxExtension -ext WixUIExtension -cultures:en-us,de-de canape.wixobj canape_files.wixobj -out canape.msi

