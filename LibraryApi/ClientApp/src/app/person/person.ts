export class Person {
  constructor(
    public firstName: string,
    public dateOfBirth: Date,
    public id?: number,
    public countryId?: number,
    public lastName?: string,
) {}
}
