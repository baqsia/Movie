import {Input, useId} from "@fluentui/react-components";
import {searchStyles} from "./styles";
import {InputOnChangeData} from "@fluentui/react-input";
import {useCallback} from "react";
import _ from 'lodash';

export interface SearchProps {
    onSearch: (search: string | null) => void
}

export const Search = (props: SearchProps) => {
    const inputId = useId('input');
    const styles = searchStyles();

    const debounceFunc = useCallback(
        _.debounce(e => search(e), 500),
        []
    );

    const search = (data: InputOnChangeData) => {
        const {value} = data;
        if (!value){
            props?.onSearch(null)
        }
        if (value && value.length > 3 && props?.onSearch) {
            props?.onSearch(value)
        }
    }

    return (
        <div className={styles.root}>
            <Input id={inputId} placeholder="Search Movie" className={styles.input}
                   onChange={(ev, data) => debounceFunc(data)}/>
        </div>)
}
