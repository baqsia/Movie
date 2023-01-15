export interface PagedResponse<TListItem> {
    totalCount: number,
    data: TListItem[]
}
