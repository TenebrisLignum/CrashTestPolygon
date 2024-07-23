export interface ChatMessagesViewModel {
    messages: ChatMessageViewModel[],
    lastMessageId: string
}

export interface ChatMessageViewModel {
    id: string,
    text: string,
    ownerId: string,
    ownerName: string,
    createdDate: Date
}