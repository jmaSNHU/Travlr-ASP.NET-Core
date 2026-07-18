var express = require('express');
var router = express.Router();

// import contact controller
var controller = require('../controllers/contact');

// GET contact page
router.get('/', controller.contact);

module.exports = router;