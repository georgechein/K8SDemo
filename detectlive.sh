#!/bin/bash
WEBSITEURL="http://k8sdemoapi/api/v1/live"
RESULT=$(curl $WEBSITEURL | grep -x "true")
if [ $RESULT ]; then
    echo ""
else
    echo "false"
fi
