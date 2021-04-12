## Prerequisites:

- You must have access to Internet
- You must have docker installed in your machine.
- You must have kubernetes installed in your machine.

## Getting Stated

### Get the Best Stories API up and running

To start the Best Stories API you must first apply the NGIX Ingress yaml file. Please open a Terminal session and execute the following command:

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v0.45.0/deploy/static/provider/cloud/deploy.yaml
```

In the same Terminal session or a new one, navigate to the `Infrastructure\k8s` directory and execute the following commands:

```bash
kubectl apply -f ./best-stories-deployment.yaml
kubectl apply -f ./ingress-service.yaml
```

### How to issue a request

To hit the new endpoint to get the 20 best stories ordered by score, please issue the following http request:

```http
GET http://localhost:80/api/v0/beststories HTTP/1.1
```
