export class Book {
  constructor(
    public title: string,
    public PublishingHouseId?: number,
    public PublishingDate?: Date,
    public Circulations?: number,
    public AuthorIds?: number[],
    public id?: number
  ) { }
}
