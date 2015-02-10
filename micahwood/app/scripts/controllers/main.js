'use strict';

/**
 * @ngdoc function
 * @name micahwoodApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the micahwoodApp
 */
angular.module('micahwoodApp')
  .controller('MainCtrl', function ($scope) {
    $scope.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
  });
