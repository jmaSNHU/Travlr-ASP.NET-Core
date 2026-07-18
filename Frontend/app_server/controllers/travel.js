const tripsEndpoint = 'https://localhost:7277/api/trips';
const options = {
    method: 'GET',
    headers: {
        'Accept': 'application/json'
    }
}

// no longer needed
// var fs = require('fs');
// var trips = JSON.parse(fs.readFileSync('./data/trips.json', 'utf8'));

// GET travel view
const travel = async function(req, res, next) {
    // console.log('TRAVEL CONTROLLER FETCHING');
    await fetch(tripsEndpoint, options)
        .then((res) => res.json())
        .then((json) => {
            // error handling code
            let message = null;
            if (!json instanceof Array) {
                message = "API lookup error";
                json = [];
            } else {
                if (!json.length) {
                    message = "No trips exists in our database!";
                }
            }
            res.render("travel", { title: "Travlr Getaways", trips: json, message})
        })
        .catch((err) => res.status(500).send(err.message));
};

module.exports = {
    travel
};