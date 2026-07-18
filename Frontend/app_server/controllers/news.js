var fs = require('fs');
var newsData = JSON.parse(fs.readFileSync('./data/news.json', 'utf8'));

// GET news view
const news = (req, res) => {
    // compiles view template for HTML response to browser
    res.render('news', { title: 'Travlr Getaways', newsData});
};

module.exports = {
    news
};