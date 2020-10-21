import requests
import json
import configparser

#Simple script to train multiple documents from file

url = 'http://localhost:8081/api/training/document'

with open('train.json','r' , encoding='utf-8') as f:
  	data = json.load(f)
for item in data:
	x = requests.post(url, json = item, verify=False)