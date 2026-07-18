var fs = require('fs');
var mealsData = JSON.parse(fs.readFileSync('./data/meals.json', 'utf8'));

// GET meals view
const meals = (req, res) => {
    // compiles view template for HTML response to browser
    res.render('meals', { title: 'Travlr Getaways', mealsData});
};

module.exports = {
    meals
};