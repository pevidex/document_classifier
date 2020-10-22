# Document Classifier

## Included Files

- README.md (this file)
- train.json (dataset)

## Task Description

The task is to write an HTTP REST web service which classifies news documents. At any given point in time, the service can receive (1) a training document already classified with a topic; (2) a test document, to which it has to return a prediction for the topic of that document. The web service should run "forever" and be ready to receive either request (1) or (2) at any given time. The topic to be predicted is 1 out of 5: "business", "entertainment", "politics", "sports" or "tech".

For the topic preditiction part, one of these two classifier algorithms can be implemented (just one):

- K-NN Classifier (<https://en.wikipedia.org/wiki/K-nearest_neighbors_algorithm>)
- Naive Bayes Classifier (<https://en.wikipedia.org/wiki/Naive_Bayes_classifier>)

HTTP API definition:

1. _Receiving training documents_

   `POST /api/training/document`

   ```json
   {
     "text": "",
     "topic": ""
   }
   ```

   The response of this API call should be just HTTP code 200 on success or an error code otherwise.

2. _Receiving test documents_

   `POST /api/test/document`

   ```json
   {
     "text": ""
   }
   ```

   The response of this API call should be a JSON with the topic classification prediction on success, or an error code otherwise. An example response follows:

   ```json
   {
     "topic": "politics"
   }
   ```

A dataset is provided in file `train.json` with training documents already classified with topics (Source: It's a modified version of the data in <http://mlg.ucd.ie/datasets/bbc.html>).

## Deliverable

You need to deliver a standalone software project, written in C# or C++. The program can use existing library/frameworks, provided those are freely available and that we can install and run the program on our side (Windows/Linux).

_Important_: The classifier algorithm part must be implemented from scratch (not by importing an existing library).

When the program starts, it should launch the web service on `http://localhost:8080` and start listening for API requests.

We'll evaluate your project submission according to:

1. Correct implementation of the web service and classifier.
2. Code quality, readability, organization and comments.
3. Scalability of the proposed approach.
4. (Bonus) If the server is terminated for any reason and then restarted, it should maintain the state (what it has learned from the training data seen so far).

## Pre Requisites

- .Net Core 3

## Run

```
dotnet run
```
