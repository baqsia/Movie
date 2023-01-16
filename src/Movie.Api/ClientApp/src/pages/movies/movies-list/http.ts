import {environment} from "../../../env";
import {MoviesListItem} from "../../../models/movie.list-item";
import axios from "axios";
import {PagedResponse} from "../../../models/paged.response";

const moviesEndpoint = '/api/movies'

export async function fetchMovies(query: string | null, page?: number, itemCount?: number) {
    try {
        const params = new URLSearchParams();
        if (query)
            params.append("query", query)
        if (page)
            params.append("page", page.toString())
        if (itemCount)
            params.append("itemCount", itemCount.toString())
        const response = await axios.get<PagedResponse<MoviesListItem>>(`${environment.baseUrl}${moviesEndpoint}?${params.toString()}`);
        return response.data;
    } catch (error) {
        return <PagedResponse<MoviesListItem>>{data: [], totalCount: 0};
    }
}
