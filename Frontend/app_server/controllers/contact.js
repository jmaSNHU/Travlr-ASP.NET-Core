var fs = require('fs');
var contactData = JSON.parse(fs.readFileSync('./data/contact.json', 'utf8'));

// GET contact view
const contact = (req, res) => {
    // compiles view template for HTML response to browser
    res.render('contact', { title: 'Travlr Getaways - Contact', contactData});
};

module.exports = {
    contact
};