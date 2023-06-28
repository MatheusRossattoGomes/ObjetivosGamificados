
const path = require("path");
const express = require("express");
const app = express(); // create express app

var options = {
  index: "inicio.html"
};

app.use(express.static("public", options));

// start express server on port 5000
app.listen(5000, () => {
  console.log("server started on port 5000");
});


