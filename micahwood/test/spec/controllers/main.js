'use strict';

describe('Controller: MainCtrl', function () {

  // load the controller's module
  beforeEach(module('cipherApp'));

  var MainCtrl,
    DecipherService;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, _DecipherService_) {
    DecipherService = _DecipherService_;
    MainCtrl = $controller('MainCtrl', {
      DecipherService: DecipherService
    });

    spyOn(DecipherService, 'decipher');
  }));


  it('should initialize the view models', function () {
    expect(MainCtrl.keyword).toBeDefined();
    expect(MainCtrl.ciphertext).toBeDefined();
    expect(MainCtrl.validChars).toBeDefined();
    expect(MainCtrl.word).toBeDefined();
    expect(MainCtrl.output).toBe(null);
  });

  it('should call decipher on the DecipherService', function() {
    MainCtrl.keyword = 'KEYWORD';
    MainCtrl.ciphertext = 'CIPHERTEXT';
    MainCtrl.decipher(true);
    expect(DecipherService.decipher).toHaveBeenCalled();
  });

  it('should pass the keyword and cyphertext models to the DecipherService', function() {
    MainCtrl.keyword = 'KEYWORD';
    MainCtrl.ciphertext = 'CIPHERTEXT';
    MainCtrl.decipher(true);
    expect(DecipherService.decipher).toHaveBeenCalledWith('KEYWORD', 'CIPHERTEXT');
  });

  it('should uppercase the text of both arguments when calling the DecipherService', function() {
    MainCtrl.keyword = 'keYWoRd';
    MainCtrl.ciphertext = 'CipHerTexT';
    MainCtrl.decipher(true);
    expect(DecipherService.decipher).toHaveBeenCalledWith('KEYWORD', 'CIPHERTEXT');
  });

  it('should not call the DecipherService with empty strings', function() {
    MainCtrl.keyword = '   ';
    MainCtrl.ciphertext = '      ';
    MainCtrl.decipher(false);
    expect(DecipherService.decipher).not.toHaveBeenCalled();
  });
});
