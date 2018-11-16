const express = require("express");
const app = express();
const port = 3001;
const fs = require("fs");
const axios = require("axios");

app.get("/", (req, res) => {
  res.send(base64_encode("/baseImage/out.png"));
});
app.post("/", (req, res) => {
  console.log(req);
  var base64Data = req.body.image.replace(/^data:image\/png;base64,/, "");
  fs.writeFile("baseImage/out.png", base64Data, "base64", function(err) {
    console.log(err);
  });
});

function base64_encode(file) {
  var bitmap = fs.readFileSync(file);
  return new Buffer(bitmap).toString("base64");
}
app.listen(port, () => {
  console.log(`Example app listening on port ${port}!`);
  axios
    .post("https://floating-fjord-95063.herokuapp.com/ip", {
      ip: require("quick-local-ip").getLocalIP4()
    })
    .catch(error => {
      console.error(error);
    });
});
