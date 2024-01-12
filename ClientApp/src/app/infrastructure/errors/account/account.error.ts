export class AccountErrorResponse extends Error {
  errorMessages: string[];

  constructor(message: string, errorMessages: string[]) {
    super(message);
    this.errorMessages = errorMessages;
  }
}
