var express = require('express');
var router = express.Router();

// import about controller
var controller = require('../controllers/about');

// GET about page
router.get('/', controller.about);

module.exports = router;