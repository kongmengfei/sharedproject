import urllib.request
import requests
import io
import PyPDF2 as p2
from requests_ntlm import HttpNtlmAuth

url = "http://sp10/Shared%20Documents/test.pdf"

response = requests.get(url, auth=HttpNtlmAuth("CONTOSO\Administrator","password"))

print(response.status_code)

PDFfile = io.BytesIO(response.content)

pdfread = p2.PdfFileReader(PDFfile)

NrPages = pdfread.getNumPages()

for i in range(NrPages):
  x = pdfread.getPage(i)
  y = str(x.extractText())
  print(y)
