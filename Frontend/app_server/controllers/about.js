// GET about view
const about = (req, res) => {
    // compiles view template for HTML response to browser
    res.render('about', { title: 'Travlr Getaways - About'});
};

module.exports = {
    about
};