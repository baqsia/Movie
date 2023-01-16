import {environment} from "../../../env";
import {MoviesListItem} from "../../../models/movie.list-item";
import axios from "axios";

const moviesEndpoint = '/api/movies'
export async function fetchMovie(id: string) {
    try {
        const response = await axios.get<MoviesListItem>(`${environment.baseUrl}${moviesEndpoint}/${id}`);
        return response.data;
    } catch (error) {
        return null;
    }
}
