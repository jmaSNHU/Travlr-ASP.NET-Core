var fs = require('fs');
var roomsData = JSON.parse(fs.readFileSync('./data/rooms.json', 'utf8'));

// GET rooms view
const rooms = (req, res) => {
    // compiles view template for HTML response to browser
    res.render('rooms', { title: 'Travlr Getaways', roomsData});
};

module.exports = {
    rooms
};