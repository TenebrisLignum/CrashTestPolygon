export interface CreateChatRoomRequest {
    name: string,
    isPrivate: boolean,
    password: string | null
}