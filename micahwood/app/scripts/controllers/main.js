'use strict';

/**
 * @ngdoc function
 * @name cipherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the cipherApp
 */
angular
  .module('cipherApp')
  .controller('MainCtrl', MainCtrl);

function MainCtrl(DecipherService) {
  var vm = this;

  vm.keyword = '';
  vm.ciphertext = '';
  vm.output = null;
  vm.validChars = /^[a-zA-z\s]*$/;
  vm.word = /^[a-zA-z]*$/;
  vm.decipher = decipher;

  //////////

  function decipher(isValid) {
    if (isValid) {
      var keyword = angular.uppercase(vm.keyword),
        ciphertext = angular.uppercase(vm.ciphertext);
      vm.output = DecipherService.decipher(keyword, ciphertext);
    }
  }
}
