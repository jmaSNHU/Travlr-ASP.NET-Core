// interface for a trip object from the backend API
export interface Trip {
    _id: string, // internal MongoDB primary key
    code: string,
    name: string, 
    length: string,
    start: Date,
    resort: string,
    perPerson: string,
    image: string, // URI or filepath to an image file
    description: string
}