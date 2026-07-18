// GET home page
const index = (req, res) => {
    // compiles a view template for HTML response to browser
    res.render('index', { title: 'Travlr Getaways'});
};

module.exports = {
    index
};

