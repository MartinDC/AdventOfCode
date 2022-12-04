import { Token, Block } from "./tokenizer";

const _seed = 5233525678;
let _aocKeyincrement = 0; // Create keys from this one, shared by both mutables and not mutables.
const cryptoRnd = () => window.crypto.getRandomValues(new Uint32Array(1))[0] / _seed;

export const randomNumber = cryptoRnd;
export const tokenEmpty = ' ';
export const tokenComma = ',';

export class AOCPartVO {
    static first: string = '0';
    static second: string = '1';
}

export abstract class AoCComponent {
    public id = new ImmutableKey();
    public tokens: Array<Token>;
    public blocks: Array<Block>;
    public rawInput: string;

    constructor(public day: string, public part: string) { }
    public abstract solve(): number;

    public init(rawInput: string, tokens: Array<Token>, blocks: Array<Block>): void {
        this.rawInput = rawInput;
        this.tokens = tokens;
        this.blocks = blocks;
    }
}

export class ImmutableKey {
    private _key = _aocKeyincrement++;

    get key() { return this._key; }
}

export class MutableKey {
    private _key = _aocKeyincrement++;

    get key() { return this._key; }
    set key(key) { this._key = key; }
}
