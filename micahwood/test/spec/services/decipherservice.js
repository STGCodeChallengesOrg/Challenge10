'use strict';

describe('Service: DecipherService', function () {

  // load the service's module
  beforeEach(module('cipherApp'));

  // instantiate service
  var DecipherService,
    keyword,
    text;
  beforeEach(inject(function (_DecipherService_) {
    DecipherService = _DecipherService_;
    keyword = 'SECRET';
    text = 'JHQSU XFXBQ';
  }));

  it('should decipher the text based on the keyword', function () {
    expect(DecipherService.decipher(keyword, text)).toEqual('CRYPT OLOGY');
  });

});
