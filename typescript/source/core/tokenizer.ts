import { tokenComma, tokenEmpty } from "./defs";

declare type TokenValueString = string;
declare type TokenValueNumber = number;
declare type TokenValue = TokenValueString | TokenValueNumber;

export class TokenizerConfig {
    split: string; // Split tokens on this character
    replace: string; // Replace token with this character
    size: number; // If this is given, this many character make a block(ie. a grouping of vital intcode program instructions)

    block: boolean; // Process should create data blocks
    number: boolean; // Convert all tokens to type number
}

export const defaultConfig: TokenizerConfig = {
    split: tokenEmpty,
    number: true,
    block: false,
    replace: null,
    size: 4
};

export const intCodeConfig: TokenizerConfig = {
    split: tokenComma,
    number: true,
    block: true,
    replace: null,
    size: 4
};

export class Block {
    id: number;
    size: number;
    tokens: Array<Token>;
}

export class Token {
    blockId: number;
    index: number;
    value: TokenValue;
    integer: TokenValueNumber;
    text: TokenValueString;
}

export class Tokenizer {
    private preProcessFn: (input: string) => string;
    private postProcessFn: (input: string) => string;

    private config: TokenizerConfig;
    private processedInput: string;
    private rawInputData: string;

    process(rawInput: string, config?: TokenizerConfig) {
        if (!rawInput || rawInput.length <= 0) {
            throw 'Tokenizer - Failed to process: No input was given!';
        }

        const tokenizerConfig = config ? config : this.config;

        this.rawInputData = rawInput;
        if (this.preProcessFn) {
            rawInput = this.preProcessFn(rawInput);
        }

        const blocks = new Array<Block>();
        const tokens: Array<Token> = new Array<Token>();
        const rawTokenizedInput = rawInput.split(tokenizerConfig.split);
        const tokenizedInput: TokenValue[] = tokenizerConfig.number ? rawTokenizedInput.map((t) => parseInt(t)) : rawTokenizedInput;

        if (tokenizedInput) {
            tokenizedInput.forEach((token: TokenValue, index: number) => {
                const blockId = tokenizerConfig.block ? Math.floor(index / tokenizerConfig.size) : null;
                const tokenObj: Token = {
                    blockId: blockId,
                    value: token,
                    index: index,

                    integer: token as TokenValueNumber,
                    text: token as TokenValueString,
                }

                tokens.push(tokenObj);
            });
        }

        if (tokenizerConfig.block && tokenizedInput && tokenizedInput.length > 0) {
            const maxBlocks = tokens[tokens.length - 1].blockId;
            for (let index = 0; index < maxBlocks + 1; index++) {
                blocks.push({
                    tokens: tokens.filter((token) => token.blockId === index),
                    size: tokenizerConfig.size,
                    id: index,
                });
            }
        }

        if (this.postProcessFn) { // TODO - Should modify tokens not the raw input
            rawInput = this.postProcessFn(rawInput);
        }

        return { tokens, blocks };
    }

    onPreProcess(preProcessFn: (input: string) => string) {
        this.preProcessFn = preProcessFn;
    }

    onPostProcess(postProcessFn: (input: string) => string) {
        this.postProcessFn = postProcessFn;
    }

    setConfig(config: TokenizerConfig) {
        this.config = config;
    }

    get input() { return this.processedInput; }
    get rawInput() { return this.rawInputData; }
}