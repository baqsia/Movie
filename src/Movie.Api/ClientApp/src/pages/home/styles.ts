import {makeStyles, shorthands} from "@fluentui/react-components";

export const movieHomeStyles = makeStyles({
    screen: {
        width: '100%',
        display: 'flex',
        alignItems: 'center',
        ...shorthands.padding('1px'),
        flexDirection: "column"
    },
    navLink: {
        color: '#efefef',
        fontSize: '1.2em'
    }
});
