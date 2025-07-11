#!/bin/bash

set -e

KONG_ADMIN="$1"  # ví dụ: https://103.221.223.177:30572

echo "⚙️ Creating service 'basket-service'..."
curl -k -s -X POST "$KONG_ADMIN/services" \
  --data "name=basket-service" \
  --data "url=http://basket-api.default.svc.cluster.local:80"

echo "⚙️ Creating route '/basket'..."
curl -k -s -X POST "$KONG_ADMIN/services/basket-service/routes" \
  --data "paths[]=/basket"

echo "✅ Kong service and route setup complete."
