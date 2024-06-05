import { AuthResponseDto } from "../interfaces/dto/auth/AuthResponseDto";

const ACCESS_TONKEN_KEY = 'access_token';
const REFRESH_TONKEN_KEY = 'refresh_token';

export default class LocalStorageHelper {
    static set(key: string, value: string) {
        localStorage.setItem(key, value);
    }

    static get(key: string): string | null {
        return localStorage.getItem(key);
    }

    static getAccessToken(): string | null {
        return this.get(ACCESS_TONKEN_KEY);
    }

    static updateTokens(tokens: AuthResponseDto) {
        this.set(ACCESS_TONKEN_KEY, tokens.accessToken);
        this.set(REFRESH_TONKEN_KEY, tokens.refreshToken);
    }
}