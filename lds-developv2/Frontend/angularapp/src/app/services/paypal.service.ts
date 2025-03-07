import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaypalService {
  private clientId = 'AZMn8TdIBBoVHFDz_YwwYFOMep1O-KmmeHqDMr4TUMuwitTtHcmeXX5wen9gBKtZzQXwc5MHq79gDfZL';

  private environment: paypal.Environment = new paypal.LiveEnvironment(this.clientId);

  constructor() { }
}
