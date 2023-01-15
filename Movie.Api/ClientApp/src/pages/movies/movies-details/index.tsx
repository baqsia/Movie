import {NavLink, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {MoviesListItem} from "../../../models/movie.list-item";
import {fetchMovie} from "./http";
import {movieDetailsStyles} from "./styles";
import * as React from "react";
import {movieYear} from "../shared";

export const MoviesDetails = () => {
    const {id} = useParams();
    const [movieDetails, setMovieDetails] = useState<MoviesListItem>()
    const [notFound, setNotFound] = useState(false);
    const styles = movieDetailsStyles();

    useEffect(() => {
        fetchMovie(id as string).then(response => {
            if (response) {
                setMovieDetails(response)
            } else
                setNotFound(true)
        })
    }, [])
    return (
        <div>
            <div className={styles.screen}>
                <NavLink to={"/movies"} style={{fontSize: "1.2em", color: "#efefef"}}>Go Back</NavLink>
                {notFound ? <div>movie not found</div>
                    : <div>
                        <h1>Movie</h1>
                        <div className={styles.details}>
                            <div>
                                <h3>Title</h3>
                                <h2>{movieDetails?.title}</h2>
                            </div>
                            <div>
                                <h3>Release Date</h3>
                                <h2>{movieYear(movieDetails?.releaseDate!)}</h2>
                            </div>
                            <div>
                                <h3>Rating</h3>
                                <h2>{movieDetails?.rating}</h2>
                            </div>
                            <div>
                                <h3>Synopsis</h3>
                                <h2>{movieDetails?.synopsis}</h2>
                            </div>
                        </div>
                    </div>}
            </div>
        </div>
    )
}
