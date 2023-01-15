import {env} from "../../../env/env.development";
import {MoviesListItem} from "../../../models/movie.list-item";
import axios from "axios";
import {PagedResponse} from "../../../models/paged.response";

const moviesEndpoint = '/api/movies'
export async function fetchMovie(id: string) {
    try {
        const response = await axios.get<MoviesListItem>(`${env.baseUrl}${moviesEndpoint}/${id}`);
        return response.data;
    } catch (error) {
        return null;
    }
}
