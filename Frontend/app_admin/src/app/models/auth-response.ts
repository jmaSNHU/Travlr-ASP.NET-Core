export class AuthResponse {
    isSuccess: boolean;
    token: string;
    message: string;

    constructor() 
    {
        this.isSuccess = false;
        this.token = '';
        this.message = '';
    }
}
