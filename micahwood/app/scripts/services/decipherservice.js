'use strict';

/**
 * @ngdoc service
 * @name cipherApp.DecipherService
 * @description
 * # DecipherService
 * Service in the cipherApp.
 */
angular
  .module('cipherApp')
  .factory('DecipherService', DecipherService);

function DecipherService() {
  var alpha = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
               'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

  var service = {
    decipher: decipher
  };
  return service;



  function decipher(keyword, text) {
    var unique = findUniqueChars(keyword),
      substAlpha = buildSubstitutionAlphabet(unique);
    return performDecipher(text, substAlpha);
  }

  function findUniqueChars(str) {
    var unique = [];
    for (var i = 0; i < str.length; i++) {
      if (unique.indexOf(str[i]) === -1) {
        unique.push(str[i]);
      }
    }
    return unique;
  }

  function buildSubstitutionAlphabet(keywordArr) {
    var columns = buildColumns(keywordArr);
    sortColumns(columns, keywordArr);
    return flattenByColumn(columns);
  }

  function buildColumns(keywordArr) {
    var columns = [],
      letters = alphabetWithout(keywordArr),
      lLength = letters.length,
      kLength = keywordArr.length;

    columns[0] = keywordArr.slice(0);
    for (var i = 0; i * kLength < lLength; i++) {
      columns[i+1] = [];
      for (var j = 0; j < kLength; j++) {
        var letter = letters[j+(i*kLength)];
        if (letter) {
          columns[i+1][j] = letter;
        }
      }
    }
    return columns;
  }

  function alphabetWithout(keywordArr) {
    // Use a copy of the alphabet array to not mutate it.
    var alphaCopy = alpha.slice(0);
    for (var i = keywordArr.length - 1; i >= 0; i--) {
      var pos = alphaCopy.indexOf(keywordArr[i]);
      if (pos > -1) {
        alphaCopy.splice(pos, 1);
      }
    }
    return alphaCopy;
  }

  function flattenByColumn(arr) {
    var flat = [],
      num;
    for (var i = 0; i < arr.length; i++) {
      for (var j = 0; j < arr.length; j++) {
        num = arr[j][i];
        if (num) {
          flat.push(num);
        }
      }
    }
    return flat;
  }

  function sortColumns(cols, keywordArr) {
    var pos;
    keywordArr.sort();
    for (var i = 0; i < keywordArr.length; i++) {
      pos = cols[0].indexOf(keywordArr[i]);
      if (pos !== i) {
        swapColumns(i, pos, cols);
      }
    }
  }

  function swapColumns(newCol, oldCol, cols) {
    var temp;
    for (var i = 0; i < cols.length; i++) {
      temp = cols[i][oldCol];
      cols[i][oldCol] = cols[i][newCol];
      cols[i][newCol] = temp;
    }
  }

  function performDecipher(message, substAlpha) {
    var messageArr = message.split('').map(function(letter) {
      if (letter !== ' ') {
        letter = alpha[substAlpha.indexOf(letter)];
      }
      return letter;
    });
    return messageArr.join('');
  }
}
