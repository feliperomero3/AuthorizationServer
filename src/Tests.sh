#!/usr/bin/env bash

curl -vk https://localhost:5000/.well-known/openid-configuration

curl -sk https://localhost:5000/.well-known/openid-configuration | jq .

curl -sk -H 'Content-Type: application/x-www-form-urlencoded' -d 'grant_type=client_credentials&scope=api1.read&client_id=oauthClient&client_secret=SuperSecretPassword' https://localhost:5000/connect/token | jq .
