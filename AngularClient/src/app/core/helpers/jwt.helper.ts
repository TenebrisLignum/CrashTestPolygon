import { jwtDecode } from "jwt-decode";
import { ACCESS_TONKEN_KEY, ROLE_CLAIM } from "../../../consts";

interface DecodedToken {
    role: string;
}

export default class JWTHelper {
    static getDecodedToken() {
        const token = localStorage.getItem(ACCESS_TONKEN_KEY);
        if (token)
            return jwtDecode<DecodedToken>(token);

        return null;
    }

    static getUserRole(): string {
        const decodedToken = this.getDecodedToken();
        if (decodedToken && decodedToken[ROLE_CLAIM]) {
            return decodedToken[ROLE_CLAIM];
        }
        return '';
    }

    static isUserInRole(role: string): boolean {
        const userRole = this.getUserRole();
        return userRole === role;
    }
}