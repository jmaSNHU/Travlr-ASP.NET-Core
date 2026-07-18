var express = require('express');
var router = express.Router();

// import meals controller
var controller = require('../controllers/meals');

// GET meals page
router.get('/', controller.meals);

module.exports = router;