export interface ChatMessagesViewModel {
    messages: ChatMessageViewModel[],
    page: number,
    hasNextPage: boolean
}

export interface ChatMessageViewModel {
    id: string,
    text: string,
    ownerId: string,
    ownerName: string,
    createdDate: Date
}